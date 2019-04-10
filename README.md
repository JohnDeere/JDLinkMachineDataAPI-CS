# JDLinkMachineDataAPI-CS
C# Sample App for JDLink Machine Data API

## System Requirments
Microsoft .Net Framework 4.7.2(Last tested) and Microsoft Visual Studio C# (community or higher)

## Setup and Run
<ul>
  <li>Update Program.cs with appId and secret from developer.deere.com..</li>
  <li>Run Program.cs file to generate OAuth tokens.</li> 
  <li>Browser/new tab should open. Login into Deere and authoize app</li> 
  <li>Copy oauth_verifier value that is in address bar of the browser and paste into terminal</li>
  <li>Hit Enter</li>
  <li>Should be able to get response from aemp/Fleet/1 endpoint. </li>
  <li>Note that in the real world you'd want to store these oAuth credentials somewhere (probably a database), and include logic to generate new oAuth credentials once you start getting HTTP 401 (Unauthorized) responses from the Deere API's.</li>
</ul>

