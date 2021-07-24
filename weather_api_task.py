#program communicates with openweathermap to get the current whether of the city entered by user

#to make a request to a web page
import requests
import sys
import urllib.request

# file that contains my api key
from config import API_Key

# url to fetch the current whether of the city
test_url="https://openweathermap.org/"
weather_endpoint = "http://api.openweathermap.org/data/2.5/weather?"

#tries to access the link to check for network connection
try:
    urllib.request.urlopen(test_url)
    city = input("Enter a city Name:")
except:
    sys.exit("Check your internet connection")

#access the url and fetchs weather data
if city.isalpha():
    #checks if the user has entered a valid city name   
    main_endpoint = weather_endpoint + "appid=" + API_Key + "&q=" + city
    
    #fetching data from main_url and 
    #json is used to convert the python dictionary data to json string
    weather_data = requests.get(main_endpoint).json()
    
    #accessing the dictionary content to check for error codes
    if weather_data['cod']=='404':
        print("The entered city name is not found")
    else:    
        print("Current Weather Data is",weather_data)

else:
    print("Please enter only characters")
