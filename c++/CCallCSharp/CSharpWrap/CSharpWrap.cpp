#include "stdafx.h"

#include "CSharpWrap.h"



// This is the main DLL file.

#include "stdafx.h"

#include "CsharpWrap.h"

extern "C" _declspec(dllexport) int DoTesting(int x, int y, char* testing, char* error)
{
	try
	{
		Test ^generator = gcnew Test();
		String^ strTesting = gcnew String(testing);
		String^ strError;

		int sum = generator->DoTesting(x, y, strTesting, strError);

		if (strError != nullptr)
		{
			char* cError =
				(char*)(Marshal::StringToHGlobalAnsi(strError)).ToPointer();
			memcpy(error, cError, strlen(cError) + 1);
		}
		else
		{
			error = nullptr;
		}

		return sum;
	}
	catch (exception e)
	{
		memcpy(error, e.what(), strlen(e.what()) + 1);
		return -1;
	}
}



