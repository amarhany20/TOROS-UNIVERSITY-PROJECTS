#include<iostream>
#include <string> 
#include <sstream>

using namespace std;
//These are my main variables
//ary is the ary that will contain the matrix of cities and distance between them
//completed is for marking the cities that are completed so that I go to each city once
//The size is 10 so, it's maximum 10 cities.
int ary[100][100], completed[100], cityAmount, cost = 0;
string cityNumberInput, citiesInput;
bool done = false;

bool check_number(string str) {
	for (int i = 0; i < str.length(); i++)
		if (isdigit(str[i]) == false)
			return false;
	return true;
}
void takeInput()
{
	int i, j;

	cout << "Enter the number of villages: ";
	while (true)
	{
		cin >> cityNumberInput;
		
		if (!check_number(cityNumberInput))
		{
			cout << "the number can't be letters it should be numbers only\n";
			continue;
		}
		else
		{
			stringstream intValue(cityNumberInput);
			intValue >> cityAmount;
			if (cityAmount == 0)
			{
				cout << "the number can't be 0 it should be more than 0\n";
				continue;
			}
			if (cityAmount>100)
			{
				cout << "the number of cities can't be more than a hundered city\n";
				continue;
			}if (cityAmount==1)
			{
				cout << "There can't be only one city there is no traveling there and no distance and no cost, this program is useless then. Please enter a number from 3 to 100\n";
				continue;
			}
			
			break;
		}
	}

	cout << "\nEnter the Cost Matrix\n";

	for (i = 0; i < cityAmount; i++)
	{
		cout << "\nEnter Elements of Row: " << i + 1 << "\n";
		bool failedinput = true;

		for (j = 0; j < cityAmount; j++)
		{			
			while (true)
			{
				cin >> citiesInput;

				if (!check_number(citiesInput))
				{
					cout << "the number can't be letters it should be numbers only\n";
					continue;
				}
				else
				{
					stringstream intValue(citiesInput);
					intValue >> ary[i][j];
					if (i == j)
					{
						if (ary[i][j] != 0)
						{
							cout << "This is the same city so, the distance should be 0 please enter 0\n";
							continue;
						}
					}
				}
				break;
			}
		}
		completed[i] = 0;
	}

	cout << "\n\nThe matrix is:";

	for (i = 0; i < cityAmount; i++)
	{
		cout << "\n\n";

		for (j = 0; j < cityAmount; j++)
			cout << "\t\t" << ary[i][j];
	}
}

int least(int currentCity)
{
	int i, nextCity = 999999;
	int min = 999999, kmin;

	for (i = 0; i < cityAmount; i++)
	{
		if ((ary[currentCity][i] != 0) && (completed[i] == 0))
			if (ary[currentCity][i] + ary[i][currentCity] < min)
			{
				min = ary[i][0] + ary[currentCity][i];
				kmin = ary[currentCity][i];
				nextCity = i;
			}
	}

	if (min != 999999){
		cost += kmin;
	}
	else
	{
		done = true;
	}
	return nextCity;
}

void mincost(int currentCity)
{
	int i, nextCity;

	completed[currentCity] = 1;

	cout << currentCity + 1 << "--->";
	nextCity = least(currentCity);

	if (done)
	{
		nextCity = 0;
		cout << nextCity + 1;
		cost += ary[currentCity][nextCity];

		return;
	}

	mincost(nextCity);
}

int main()
{
	takeInput();

	cout << "\n\nThe shortest Path from city 1 back to city 1 again through all cities is is:\n";
	mincost(0); //passing 0 because starting vertex

	cout << "\n\nThe Least cost is " << cost;

	return 0;
}