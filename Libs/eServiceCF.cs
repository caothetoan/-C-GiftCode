﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.1.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="eServiceCFSoap", Namespace="http://tempuri.org/")]
public partial class eServiceCF : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback GetPlayInfoOperationCompleted;
    
    private System.Threading.SendOrPostCallback GiftItemOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetUserInfoOperationCompleted;
    
    /// <remarks/>
    public eServiceCF() {
        if (System.Configuration.ConfigurationManager.AppSettings["SERVICE_PATH"] != null)
            this.Url = System.Configuration.ConfigurationManager.AppSettings["SERVICE_PATH"].ToString();
        else
            this.Url = "http://117.103.192.10:8002/cfNgayVang/eServiceCF.asmx";
    }
    
    /// <remarks/>
    public event GetPlayInfoCompletedEventHandler GetPlayInfoCompleted;
    
    /// <remarks/>
    public event GiftItemCompletedEventHandler GiftItemCompleted;
    
    /// <remarks/>
    public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPlayInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public CFPlayInfo GetPlayInfo(string AccountID, System.DateTime FromDate, System.DateTime ToDate) {
        object[] results = this.Invoke("GetPlayInfo", new object[] {
                    AccountID,
                    FromDate,
                    ToDate});
        return ((CFPlayInfo)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetPlayInfo(string AccountID, System.DateTime FromDate, System.DateTime ToDate, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetPlayInfo", new object[] {
                    AccountID,
                    FromDate,
                    ToDate}, callback, asyncState);
    }
    
    /// <remarks/>
    public CFPlayInfo EndGetPlayInfo(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((CFPlayInfo)(results[0]));
    }
    
    /// <remarks/>
    public void GetPlayInfoAsync(string AccountID, System.DateTime FromDate, System.DateTime ToDate) {
        this.GetPlayInfoAsync(AccountID, FromDate, ToDate, null);
    }
    
    /// <remarks/>
    public void GetPlayInfoAsync(string AccountID, System.DateTime FromDate, System.DateTime ToDate, object userState) {
        if ((this.GetPlayInfoOperationCompleted == null)) {
            this.GetPlayInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPlayInfoOperationCompleted);
        }
        this.InvokeAsync("GetPlayInfo", new object[] {
                    AccountID,
                    FromDate,
                    ToDate}, this.GetPlayInfoOperationCompleted, userState);
    }
    
    private void OnGetPlayInfoOperationCompleted(object arg) {
        if ((this.GetPlayInfoCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetPlayInfoCompleted(this, new GetPlayInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GiftItem", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public long GiftItem(string AccountID, int GiftCode, string GiftKey) {
        object[] results = this.Invoke("GiftItem", new object[] {
                    AccountID,
                    GiftCode,
                    GiftKey});
        return ((long)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGiftItem(string AccountID, int GiftCode, string GiftKey, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GiftItem", new object[] {
                    AccountID,
                    GiftCode,
                    GiftKey}, callback, asyncState);
    }
    
    /// <remarks/>
    public long EndGiftItem(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((long)(results[0]));
    }
    
    /// <remarks/>
    public void GiftItemAsync(string AccountID, int GiftCode, string GiftKey) {
        this.GiftItemAsync(AccountID, GiftCode, GiftKey, null);
    }
    
    /// <remarks/>
    public void GiftItemAsync(string AccountID, int GiftCode, string GiftKey, object userState) {
        if ((this.GiftItemOperationCompleted == null)) {
            this.GiftItemOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGiftItemOperationCompleted);
        }
        this.InvokeAsync("GiftItem", new object[] {
                    AccountID,
                    GiftCode,
                    GiftKey}, this.GiftItemOperationCompleted, userState);
    }
    
    private void OnGiftItemOperationCompleted(object arg) {
        if ((this.GiftItemCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GiftItemCompleted(this, new GiftItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public CFUserInfo GetUserInfo(string AccountID) {
        object[] results = this.Invoke("GetUserInfo", new object[] {
                    AccountID});
        return ((CFUserInfo)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetUserInfo(string AccountID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetUserInfo", new object[] {
                    AccountID}, callback, asyncState);
    }
    
    /// <remarks/>
    public CFUserInfo EndGetUserInfo(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((CFUserInfo)(results[0]));
    }
    
    /// <remarks/>
    public void GetUserInfoAsync(string AccountID) {
        this.GetUserInfoAsync(AccountID, null);
    }
    
    /// <remarks/>
    public void GetUserInfoAsync(string AccountID, object userState) {
        if ((this.GetUserInfoOperationCompleted == null)) {
            this.GetUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
        }
        this.InvokeAsync("GetUserInfo", new object[] {
                    AccountID}, this.GetUserInfoOperationCompleted, userState);
    }
    
    private void OnGetUserInfoOperationCompleted(object arg) {
        if ((this.GetUserInfoCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
public partial class CFPlayInfo {
    
    private int playTimeField;
    
    private int expField;
    
    private int killField;
    
    private int missionField;
    
    private bool isGPItemField;
    
    private int item1_CntField;
    
    private int item2_CntField;
    
    /// <remarks/>
    public int PlayTime {
        get {
            return this.playTimeField;
        }
        set {
            this.playTimeField = value;
        }
    }
    
    /// <remarks/>
    public int Exp {
        get {
            return this.expField;
        }
        set {
            this.expField = value;
        }
    }
    
    /// <remarks/>
    public int Kill {
        get {
            return this.killField;
        }
        set {
            this.killField = value;
        }
    }
    
    /// <remarks/>
    public int Mission {
        get {
            return this.missionField;
        }
        set {
            this.missionField = value;
        }
    }
    
    /// <remarks/>
    public bool isGPItem {
        get {
            return this.isGPItemField;
        }
        set {
            this.isGPItemField = value;
        }
    }
    
    /// <remarks/>
    public int Item1_Cnt {
        get {
            return this.item1_CntField;
        }
        set {
            this.item1_CntField = value;
        }
    }
    
    /// <remarks/>
    public int Item2_Cnt {
        get {
            return this.item2_CntField;
        }
        set {
            this.item2_CntField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
public partial class CFUserInfo {
    
    private long usnField;
    
    private string accountNameField;
    
    private string nickNameField;
    
    private int levField;
    
    /// <remarks/>
    public long usn {
        get {
            return this.usnField;
        }
        set {
            this.usnField = value;
        }
    }
    
    /// <remarks/>
    public string AccountName {
        get {
            return this.accountNameField;
        }
        set {
            this.accountNameField = value;
        }
    }
    
    /// <remarks/>
    public string NickName {
        get {
            return this.nickNameField;
        }
        set {
            this.nickNameField = value;
        }
    }
    
    /// <remarks/>
    public int Lev {
        get {
            return this.levField;
        }
        set {
            this.levField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void GetPlayInfoCompletedEventHandler(object sender, GetPlayInfoCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetPlayInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetPlayInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public CFPlayInfo Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((CFPlayInfo)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void GiftItemCompletedEventHandler(object sender, GiftItemCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GiftItemCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GiftItemCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public long Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((long)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void GetUserInfoCompletedEventHandler(object sender, GetUserInfoCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetUserInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public CFUserInfo Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((CFUserInfo)(this.results[0]));
        }
    }
}