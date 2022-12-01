# TollFeeCalculator 

### **Application purpose**

The purpose of the application is to calculate the fee on road-tolls in Gothenburg. 

Expected prices for different times during the day are as follows:

|     Time      |      Fee      |
| :-----------: | :-----------: |
| 06:00 - 06:29 |     9 SEK     |
| 06:30 - 06:59 |    14 SEK     |
| 07:00 - 07:59 |    18 SEK     |
| 08:00 - 08:29 |    14 SEK     |
| 08:30 – 14:59 |     9 SEK     |
| 15:00 – 15:29 |    14 SEK     |
| 15:30 – 16:59 |    18 SEK     |
| 17:00 – 17:59 |    14 SEK     |
| 18:00 – 18:29 |     9 SEK     |
| 18:30 – 05:59 |     0 SEK     |

In addition to the times above, the entirety of Saturday and Sunday is free. 

The application can recive a comma separated list of DateTime strings which represents each time a single car have passed through the tolls. 

The maximum fee that can be debited per car and day is 60 SEK. Additionaly a car that passes through several times within 60 minutes should only be debited once for those 60 minutes, in that case it is the highest price during those 60 minutes that counts.

The application should print what the total fee for the test-data should amount to.

### **Code test**

The task we ask you to perform in this test is to refactor the existing code and at the same time to write unit tests that validate the business logic. 

The existing code have several bugs, these should be identified, solved and preferably be covered by a test.
All times should be in UTC. The testData.txt file should not be modified, everything else is OK to change.

In summary the task is to
 - Refactor code so that it is clean and readable
 - Write unit tests 
 - Make sure that the provided test data produce expected results

### **Additional challange** (Optional)

If there is time to spare, a welcome addition to the solution would be a simple WebApi endpoint where one could send a GET request with a DateTime query parameter which would return the price of the given DateTime. Another endpoint should return the total for a given day by passing a set of strings in the same format as the data in testData.txt, but encoded in url friendly base64.

example endpoints:

```
/tollFee?dateTime=\<url friendly input>

/tollFee/total?dates=\<url friendly input>
```

The solution could also be upgraded to a newer version of dotnet.

In summary
 - (Optional) Migrate to newer version of dotnet
 - (Optional) WebApi with GET endpoint at /tollFee to return price for that date and time
 - (Optional) WebApi with GET endpoint at /tollFee/total to return total price a set of DateTimes