# TeamToGather
A platform for creative people who want to start a project togther


bugs
-----
- you need to change registration text areas to be required, because you can not enter a username and email and 
  it will still be moving to the next level of the registration page
- you need to fix the postback issue when the user refresehes the page, the ID is incremented by 1 and for some reason it triggers the button




things to work on
-------------------------------
- save EACH INPUT FROM THE USER **and save it on Viewstate" 
- PROBLEM - Controls don't run on server, read this : https://stackoverflow.com/questions/21329836/asp-net-cant-find-controls-added-from-code-behind-in-a-div

refrence to subjects
---------------------
- User Control tutorial - https://www.c-sharpcorner.com/UploadFile/0c1bb2/creating-user-control-in-Asp-Net/
- add an attribute dynamically to a control in the page(div in your case) - https://stackoverflow.com/questions/17858742/how-to-add-custom-attributes-to-asp-net-controls
- add a control to a div/control - https://devnet.kentico.com/questions/how-to-dynamically-add-controls-to-a-form
- how to check all of the validators in the page using Page.IsValid - https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.page.isvalid?view=netframework-4.8
- Detecting Refresh or Postback in ASP.NET https://www.codeproject.com/Articles/68371/Detecting-Refresh-or-Postback-in-ASP-NET
- ViewState https://www.c-sharpcorner.com/UploadFile/225740/what-is-view-state-and-how-it-works-in-Asp-Net53/
  - further reading https://weblogs.asp.net/infinitiesloop/truly-understanding-viewstate
- labels as radio buttons https://stackoverflow.com/questions/9207315/asp-net-creating-dynamic-radio-button-list
- Page cache - http://www.4guysfromrolla.com/webtech/111500-1.shtml
  - more: https://www.tutorialspoint.com/asp.net/asp.net_data_caching.htm

remember
--------
- in order to make an element visible in "behind code" on asp.net add **runat="server"** to it's attributes.
- IN EVERY GUEST PAGE do Session["DivID"].ABANDON because it will keep running if the user have clicked on one of the nav items.
