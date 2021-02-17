package config

type System struct {
	Mode string `mapstructure:"mode" json:"mode" ini:"mode"`
}

type Uri struct {
	Path    string            `json:"path" `
	Body    string            `json:"body"`
	Headers map[string]string `json:"headers"`
}

type Config struct {
	Server string `json:"server"`
	Uris   []Uri  `json:"uris"`
}
