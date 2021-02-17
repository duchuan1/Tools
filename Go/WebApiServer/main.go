package main

import (
	"HttpServer/config"
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"net/http"
	"os"
	"path"
	"path/filepath"
	"time"

	log1 "github.com/sirupsen/logrus"
)

var conf *config.Config
var (
	log     *log1.Logger
	logFile *os.File
	logDir  string = "logs"
)

func init() {
	initLog()
	conf = &config.Config{Server: ":8080", Uris: []config.Uri{
		{Path: "/", Body: "{\"code\":\"200\",\"msg\":\"success\"}", Headers: map[string]string{"Content-Type": "application/json"}},
	}}
	dir, _ := os.Getwd()
	confPath := filepath.Join(dir, "conf.json")
	if !IsExist(confPath) {
		s2, _ := json.MarshalIndent(*conf, "", "  ")

		ioutil.WriteFile(confPath, []byte(s2), os.FileMode(0666))
	}
	bConf, e := ioutil.ReadFile(confPath)
	if e != nil {
		log.Println("Error", e)
	}
	json.Unmarshal(bConf, conf)
	// sConf := string(bConf)
	// log.Println("conf", confPath, sConf)
	// s1, _ := json.MarshalIndent(*conf, "", "  ")
	// log.Println(string(s1))

	s1, _ := json.Marshal(*conf)
	log.Info(string(s1))
	initHandle()
}
func initLog() {
	os.MkdirAll(logDir, os.ModePerm)
	// t := time.Now().Format("20060102150405")
	t := time.Now().Format("2006010215")

	logPath := path.Join(logDir, "log_"+t+".log")
	logFile, _ = os.OpenFile(logPath, os.O_CREATE|os.O_WRONLY|os.O_APPEND, 0666)
	if logFile == nil {
		log1.Fatalln("Open Log File Error")
	}

	log = log1.New()
	log.Out = io.MultiWriter(os.Stdout, logFile)
}
func initHandle() {
	for _, _uri := range conf.Uris {
		http.HandleFunc(_uri.Path, func(w http.ResponseWriter, r *http.Request) {

			rBody, _ := ioutil.ReadAll(r.Body)
			sBody := string(rBody)
			// log.Println("Req: ", r.URL.Path, r.Method, r.Header, sBody)
			log.WithFields((log1.Fields{
				"Path":    r.URL.Path,
				"Method":  r.Method,
				"Headers": r.Header,
				"Body":    sBody,
			})).Info("Req")

			uri := findURIByPath(r.URL.Path)
			if uri == nil {
				w.Header().Set("Content-Type", "application/json")
				rspBody := "{\"code\":\"200\",\"msg\":\"success\"}"
				fmt.Fprintf(w, rspBody)
				// log.Println("Unknown Path: " + r.URL.Path + ", Use Default")
				log.WithFields((log1.Fields{
					"Path":    r.URL.Path,
					"Headers": w.Header(),
					"Body":    rspBody,
				})).Info("Rsp")

				return
			}

			for k, v := range uri.Headers {
				w.Header().Set(k, v)
			}
			fmt.Fprintf(w, uri.Body)
			// log.Println("Rsp", w.Header(), uri.Body)
			log.WithFields((log1.Fields{
				"Path":    r.URL.Path,
				"Headers": w.Header(),
				"Body":    uri.Body,
			})).Info("Rsp")
		})
	}
}
func findURIByPath(path string) (uri *config.Uri) {
	uri = nil
	for _, _uri := range conf.Uris {
		if _uri.Path == path {
			uri = &_uri
			break
		}
	}

	return uri
}
func main() {
	defer logFile.Close()

	log.WithFields(log1.Fields{"Server": conf.Server}).Info("Start")
	log.Fatal(http.ListenAndServe(conf.Server, nil))
}

// IsExist checks whether a file or directory exists.
// It returns false when the file or directory does not exist.
func IsExist(f string) bool {
	_, err := os.Stat(f)
	return err == nil || os.IsExist(err)
}
