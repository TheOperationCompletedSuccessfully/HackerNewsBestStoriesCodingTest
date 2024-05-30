# Hacker News Best Stories Coding Test

There are several ways to run it:
1. In Visual Studio click run button and it should open swagger ui where you can test API calls
2. Use Postman app
3a. Open HackerNewsBestStories.http in Visual Studio
3b. Click start without debugging
3c. Click "send request" hint above "GET {{schema}}://{{hostname}}:{{port}}/HackerNewsBestStory/api/v0/takefirst/45" line

Assuming that data from hacker-news.firebaseio.com will change, but not so quick
I used [ResponseCache(Duration = 3600)] to keep responding the same data for the same value of n param for an hour.
Also the requests for more than 1000 items will get 400 response

However If I had more time 
there could be created L2 Cache (MemoryCache) 
tha would enhance the granularity for next requests,
e.g. that would keep cache data and asked firebase for differences only (if any),
for instance 
after request for 30 items => all of them cached
next request for 100 items will produce only 70 calls (delta).
