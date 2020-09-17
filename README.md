# TeamToGather
A platform for creative people who want to start a project togther


bugs
-----
- you need to change registration text areas to be required, because you can not enter a username and email and 
  it will still be moving to the next level of the registration page
- you need to fix the postback issue when the user refresehes the page, the ID is incremented by 1 and for some reason it triggers the button




things to work on/things I did last night and it deleted
-------------------------------
- make a method that checks if the current username passed in registration is used, if it is, dont allow it and notify the user. 

refrence to subjects
---------------------
- User Control tutorial - https://www.c-sharpcorner.com/UploadFile/0c1bb2/creating-user-control-in-Asp-Net/
- add an attribute dynamically to a control in the page(div in your case) - https://stackoverflow.com/questions/17858742/how-to-add-custom-attributes-to-asp-net-controls
- add a control to a div/control - https://devnet.kentico.com/questions/how-to-dynamically-add-controls-to-a-form
- how to check all of the validators in the page using Page.IsValid - https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.page.isvalid?view=netframework-4.8
- Detecting Refresh or Postback in ASP.NET https://www.codeproject.com/Articles/68371/Detecting-Refresh-or-Postback-in-ASP-NET

remember
--------
- in order to make an element visible in "behind code" on asp.net add **runat="server"** to it's attributes.
