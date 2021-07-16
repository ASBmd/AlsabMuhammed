
function nameValid(){
    var name=document.getElementById("nameText").value 
    var nameSpan=document.getElementById("nameSpan");
    var letters= /^[a-zA-Z\s]*$/;
    if(name==""){
        nameSpan.innerHTML="Filed is required"
        return false;}
    else if(name.match(letters)){
        nameSpan.innerHTML="";
        return true;
    }
    else{
        nameSpan.innerHTML="use only characters";
        return false;
    }
    
}
function PhoneValid(){
    var phone=document.getElementById("phoneText").value 
    var phoneSpan=document.getElementById("phoneSpan");
    var letters= /^\d{10}$/;
    if(phone==""){
        phoneSpan.innerHTML="Filed is required"
        return false;}
    else if(phone.match(letters)){
        phoneSpan.innerHTML="";
        return true;
    }
    else{
        phoneSpan.innerHTML="invalid number";
        return false;
    }
    
}
function emailValid(){
    var name=document.getElementById("emailText").value 
    var emailSpan=document.getElementById("emailSpan");
    var letters=  /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if(name==""){
        emailSpan.innerHTML="Filed is required"
        return false;}
    else if(name.match(letters)){
        emailSpan.innerHTML="";
        return true;
    }
    else{
        emailSpan.innerHTML="invalid email";
        return false;
    }
 
}
function MessageValid(){
    var Text=document.getElementById("msgText").value 
    var msgSpan=document.getElementById("msgSpan");
    
    if(Text==""){
        msgSpan.innerHTML="Filed is required"
        return false;}
    else if(Text.length<10){
        msgSpan.innerHTML="Enter minimum 10 character";
        return false;
    }
    else{
        msgSpan.innerHTML="";
        return true;
    }
 
}