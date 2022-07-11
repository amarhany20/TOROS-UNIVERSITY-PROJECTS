#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <regex>
using namespace std;

//refrence for functions because I am used to putting functions under main
void OpenFile(ifstream&, string);
void ProcessFile(ifstream&);
void GetStringsToArray(string,int);
void RemoveDublicates();
void CheckEachIpLogTimes();
void SortingIpArrayWithLogCount();
//public variables or global variables
string logArray[4000][2];
string ipArrayWithLogCount[4000][2];

int main()
{
	
	//Opening the file 
	ifstream inFile;
	OpenFile(inFile, "C:\\Users\\ammar\\OneDrive\\Current Files\\Finished\\Sent\\Ammar Hany Ezeldin Abdelrazik_195050902_C++\\Midterm Project with dynamic link\\log.txt");
	//Process The file
	ProcessFile(inFile);
	//Closing the file 
	inFile.close();//it was said that this is the best practice that I should close everyfile after using it 
	RemoveDublicates();
	CheckEachIpLogTimes();
	cout << "------------------------------------------\nAll the ips that signed in to the server sorted in descending order with the count of logs: " << endl;
	SortingIpArrayWithLogCount();
	cout << "------------------------------------------\nSearch the specific Ip address which you want to check if it logged in before and the number of logs: " << endl;
	while (true)
	{
		string ip;
		cout << "Enter the IP: ";
		cin >> ip ;
		bool ipAvalaible = false;
		int ipIndex = 0;
		for (int i = 0; i < 4000; i++)
		{
			if (ip == ipArrayWithLogCount[i][0])
			{
				ipAvalaible = true;
				ipIndex = i;
				break;
			}
		}
		if (ipAvalaible)
		{
			cout << "------------------------------------------\n" << " _-_ IP FOUND _-_ " << "\nIP: " << ipArrayWithLogCount[ipIndex][0] << " Log Count: " << ipArrayWithLogCount[ipIndex][1] <<"\n------------------------------------------\n";
			ipAvalaible = false;
			ipIndex = 0;
		}
		else
		{
			cout << "------------------------------------------\nThis IP hasn't logged in the server before. It's not available.\n------------------------------------------\n" << endl;
		}
	}
}

void OpenFile(ifstream& inFile, string fileName) {
	inFile.open(fileName);
	if (inFile.is_open())
	{
		cout << "File Opened Successfully" << endl;// it will tell us that the file opened successfully 
	}
	else
	{
		cout << "Failed to open file"<<endl; //it will tell us the the file failed to open
		exit(-1); //This will close the program
	}
}

void ProcessFile(ifstream& inFile) {
	//In here we are reading line by line in the file
	string line;
	int counter = 0;
	while (!inFile.eof())//eof: end of file
	{
		getline(inFile, line);
		if (inFile.good())//to make sure we read a line (it's a good practice)
		{
			/*cout<<counter<<" " << line << endl;*/
			GetStringsToArray(line,counter);
			counter++;
		}
	}
	//for testing the array and checking if it's all in here or not
	/*for (int i = 0; i < 4000; i++)
	{
		for (int j = 0; j < 2; j++)
		{
			cout<<i<<" " << j<<":" << logArray[i][j] << " ";
		}
		cout << "\n";
	}*/
}

void GetStringsToArray(string line,int counter) {
	string ip;
	regex expr("((?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))");
	 std::regex rgx(".*FILE_(\\w+)_EVENT\\.DAT.*");
    std::smatch match;

	if (std::regex_search(line, match, expr))
	{
		std::cout << "match: " << match[0] << '\n';
		ip = match[0];
		logArray[counter][0] = ip;
		logArray[counter][1] = line;
	}
	else
		cout << "Failed: \n" << line;
	/*string delimiter = "-";
	 ip = line.substr(0, line.find(delimiter));
	ip.erase(remove(ip.begin(), ip.end(), '\"'), ip.end());
	ip.erase(ip.begin(), find_if(ip.begin(), ip.end(), [](unsigned char ch) {return !std::isspace(ch);}));
	ip.erase(std::find_if(ip.rbegin(), ip.rend(), [](unsigned char ch) {return !std::isspace(ch);}).base(), ip.end());
	string log = line.substr(line.find(delimiter));
	logArray[counter][0] = ip;
	logArray[counter][1] = line;
		*/

	

}

void RemoveDublicates() {
	bool dontAddTrigger = false;
	int index = 0;
	for (int i = 0; i < 4000; i++)
	{
		
			for (size_t j = 0; j < 4000; j++)
			{
				if (ipArrayWithLogCount[j][0] != logArray[i][0])
				{

				}
				else
				{
					dontAddTrigger = true;
					break;
				}
			}
			if (!dontAddTrigger)
			{
				ipArrayWithLogCount[index][0] = logArray[i][0];
				index++;
			}
			if (dontAddTrigger)
			{
				dontAddTrigger = false;
			}
	}//for checking if I only got all the ips or not
	/*for (size_t i = 0; i < 4000; i++)
	{
		if (!ipArrayWithLogCount[i][0].empty())
		{
			cout << ipArrayWithLogCount[i][0] << endl;
		}
		
	}*/
			
				
				
			
			
		
		
	
}

void CheckEachIpLogTimes() {
	int setAmount = 0;
	for (int i = 0; i < 4000; i++)
	{
		for (int j = 0; j < 4000; j++)
		{
			if (ipArrayWithLogCount[i][0] == logArray[j][0] && !ipArrayWithLogCount[i][0].empty())
			{
				setAmount++;
			}
		}
		if (setAmount!=0)
		{
			ipArrayWithLogCount[i][1] = to_string(setAmount);
			setAmount = 0;
		}
	}//for checking if I got the logs for each IP
	/*for (size_t i = 0; i < 4000; i++)
	{
		if (!ipArrayWithLogCount[i][0].empty())
		{
			cout << ipArrayWithLogCount[i][0] << "- Amount Of Logs: "<< ipArrayWithLogCount[i][1] <<endl;
		}

	}*/
}

void SortingIpArrayWithLogCount() {
	/*cout << "Sorting Time" << endl;*/
	for (int i = 0; i < 4000; i++)
	{
		for (int j = i + 1; j < 4000; j++)
		{
			if (!ipArrayWithLogCount[i][0].empty() && !ipArrayWithLogCount[j][0].empty())
			{
				if (stoi(ipArrayWithLogCount[i][1]) < stoi(ipArrayWithLogCount[j][1]))
				{
					string temp = ipArrayWithLogCount[i][1];
					string iptemp = ipArrayWithLogCount[i][0];
					ipArrayWithLogCount[i][1] = ipArrayWithLogCount[j][1];
					ipArrayWithLogCount[i][0] = ipArrayWithLogCount[j][0];
					ipArrayWithLogCount[j][1] = temp;
					ipArrayWithLogCount[j][0] = iptemp;

				}
			}
			
		}
	}for (size_t i = 0; i < 4000; i++)//checking if sorting is done and working well
	{
		if (!ipArrayWithLogCount[i][0].empty())
		{
			cout<<"Ip: " << ipArrayWithLogCount[i][0] << "- Count Of Logs: " << ipArrayWithLogCount[i][1] << endl;
		}

	}


}