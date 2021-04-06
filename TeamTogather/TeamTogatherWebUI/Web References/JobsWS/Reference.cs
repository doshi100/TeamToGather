﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace TeamTogatherWebUI.JobsWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="JobsWSSoap", Namespace="http://tempuri.org/")]
    public partial class JobsWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ReturnJobOffersOperationCompleted;
        
        private System.Threading.SendOrPostCallback LogInOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddJobOfferOperationCompleted;
        
        private System.Threading.SendOrPostCallback countOffersOperationCompleted;
        
        private System.Threading.SendOrPostCallback getUserFNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public JobsWS() {
            this.Url = global::TeamTogatherWebUI.Properties.Settings.Default.TeamTogatherWebUI_JobsWS_JobsWS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ReturnJobOffersCompletedEventHandler ReturnJobOffersCompleted;
        
        /// <remarks/>
        public event LogInCompletedEventHandler LogInCompleted;
        
        /// <remarks/>
        public event AddJobOfferCompletedEventHandler AddJobOfferCompleted;
        
        /// <remarks/>
        public event countOffersCompletedEventHandler countOffersCompleted;
        
        /// <remarks/>
        public event getUserFNameCompletedEventHandler getUserFNameCompleted;
        
        /// <remarks/>
        public event GetUserCompletedEventHandler GetUserCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReturnJobOffers", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public JobOffer[] ReturnJobOffers() {
            object[] results = this.Invoke("ReturnJobOffers", new object[0]);
            return ((JobOffer[])(results[0]));
        }
        
        /// <remarks/>
        public void ReturnJobOffersAsync() {
            this.ReturnJobOffersAsync(null);
        }
        
        /// <remarks/>
        public void ReturnJobOffersAsync(object userState) {
            if ((this.ReturnJobOffersOperationCompleted == null)) {
                this.ReturnJobOffersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReturnJobOffersOperationCompleted);
            }
            this.InvokeAsync("ReturnJobOffers", new object[0], this.ReturnJobOffersOperationCompleted, userState);
        }
        
        private void OnReturnJobOffersOperationCompleted(object arg) {
            if ((this.ReturnJobOffersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReturnJobOffersCompleted(this, new ReturnJobOffersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LogIn", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool LogIn(string username, string password) {
            object[] results = this.Invoke("LogIn", new object[] {
                        username,
                        password});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LogInAsync(string username, string password) {
            this.LogInAsync(username, password, null);
        }
        
        /// <remarks/>
        public void LogInAsync(string username, string password, object userState) {
            if ((this.LogInOperationCompleted == null)) {
                this.LogInOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogInOperationCompleted);
            }
            this.InvokeAsync("LogIn", new object[] {
                        username,
                        password}, this.LogInOperationCompleted, userState);
        }
        
        private void OnLogInOperationCompleted(object arg) {
            if ((this.LogInCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogInCompleted(this, new LogInCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddJobOffer", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddJobOffer(string username, string password, string phone, string company, string pos) {
            this.Invoke("AddJobOffer", new object[] {
                        username,
                        password,
                        phone,
                        company,
                        pos});
        }
        
        /// <remarks/>
        public void AddJobOfferAsync(string username, string password, string phone, string company, string pos) {
            this.AddJobOfferAsync(username, password, phone, company, pos, null);
        }
        
        /// <remarks/>
        public void AddJobOfferAsync(string username, string password, string phone, string company, string pos, object userState) {
            if ((this.AddJobOfferOperationCompleted == null)) {
                this.AddJobOfferOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddJobOfferOperationCompleted);
            }
            this.InvokeAsync("AddJobOffer", new object[] {
                        username,
                        password,
                        phone,
                        company,
                        pos}, this.AddJobOfferOperationCompleted, userState);
        }
        
        private void OnAddJobOfferOperationCompleted(object arg) {
            if ((this.AddJobOfferCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddJobOfferCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/countOffers", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int countOffers() {
            object[] results = this.Invoke("countOffers", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void countOffersAsync() {
            this.countOffersAsync(null);
        }
        
        /// <remarks/>
        public void countOffersAsync(object userState) {
            if ((this.countOffersOperationCompleted == null)) {
                this.countOffersOperationCompleted = new System.Threading.SendOrPostCallback(this.OncountOffersOperationCompleted);
            }
            this.InvokeAsync("countOffers", new object[0], this.countOffersOperationCompleted, userState);
        }
        
        private void OncountOffersOperationCompleted(object arg) {
            if ((this.countOffersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.countOffersCompleted(this, new countOffersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getUserFName", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getUserFName(string username) {
            object[] results = this.Invoke("getUserFName", new object[] {
                        username});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getUserFNameAsync(string username) {
            this.getUserFNameAsync(username, null);
        }
        
        /// <remarks/>
        public void getUserFNameAsync(string username, object userState) {
            if ((this.getUserFNameOperationCompleted == null)) {
                this.getUserFNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUserFNameOperationCompleted);
            }
            this.InvokeAsync("getUserFName", new object[] {
                        username}, this.getUserFNameOperationCompleted, userState);
        }
        
        private void OngetUserFNameOperationCompleted(object arg) {
            if ((this.getUserFNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUserFNameCompleted(this, new getUserFNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public User GetUser(string username) {
            object[] results = this.Invoke("GetUser", new object[] {
                        username});
            return ((User)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserAsync(string username) {
            this.GetUserAsync(username, null);
        }
        
        /// <remarks/>
        public void GetUserAsync(string username, object userState) {
            if ((this.GetUserOperationCompleted == null)) {
                this.GetUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserOperationCompleted);
            }
            this.InvokeAsync("GetUser", new object[] {
                        username}, this.GetUserOperationCompleted, userState);
        }
        
        private void OnGetUserOperationCompleted(object arg) {
            if ((this.GetUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserCompleted(this, new GetUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class JobOffer {
        
        private int offerIDField;
        
        private int userIDField;
        
        private string firstNameField;
        
        private string phoneField;
        
        private string companyField;
        
        private string positionField;
        
        /// <remarks/>
        public int OfferID {
            get {
                return this.offerIDField;
            }
            set {
                this.offerIDField = value;
            }
        }
        
        /// <remarks/>
        public int UserID {
            get {
                return this.userIDField;
            }
            set {
                this.userIDField = value;
            }
        }
        
        /// <remarks/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        public string Company {
            get {
                return this.companyField;
            }
            set {
                this.companyField = value;
            }
        }
        
        /// <remarks/>
        public string Position {
            get {
                return this.positionField;
            }
            set {
                this.positionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class User {
        
        private int idField;
        
        private string firstNameField;
        
        private string usernameField;
        
        private string passwordField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <remarks/>
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void ReturnJobOffersCompletedEventHandler(object sender, ReturnJobOffersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReturnJobOffersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReturnJobOffersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public JobOffer[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((JobOffer[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void LogInCompletedEventHandler(object sender, LogInCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogInCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogInCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void AddJobOfferCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void countOffersCompletedEventHandler(object sender, countOffersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class countOffersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal countOffersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void getUserFNameCompletedEventHandler(object sender, getUserFNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getUserFNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getUserFNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void GetUserCompletedEventHandler(object sender, GetUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public User Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((User)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591