# JDLinkMachineDataAPI-CS
C# Sample App for JDLink Machine Data API

## System Requirments
Microsoft .Net Framework 4.6.2(Last tested) and Microsoft Visual Studio C# (community or higher)

## Setup and Run
<ul>
  <li>Update ApiCredentials.cs with appId and secret from developer.deere.com.</li>
  <li>Run Program.cs file to generate OAuth tokens. Update the generated tokens in ApiCredentials.cs "TOKEN" variable</li>
  <li> To fetch Fleet details make sure to comment the below lines from Program.cs file: 
     <ul>
      <li>of.retrieveOAuthProviderDetails();                        //Line 1</li>
      <li>of.getRequestToken();                                     //Line 2</li>                       
      <li>of.authorizeRequestToken();                               //Line 3</li>                          
      <li>of.exchangeRequestTokenForAccessToken();                  //Line 4</li>       
    </ul>
  </li>
  <li>Then uncomment these lines and run the Program.cs file.
  <ul>
    <li>//FleetInfo fi = new FleetInfo();                           //Line 5</li>
    <li>//fi.retrieveFleetDetails();                                // Line 6</li>
    </ul>
  </li>
</ul>

