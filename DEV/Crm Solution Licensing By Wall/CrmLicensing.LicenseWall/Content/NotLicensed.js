//#notlicensed
try
{
    CLBW.LicensedResource.callback(false, null);
}
catch (e) {
    alert("You organisation has not been authorised\n Please contact the solution publisher.");
    document.body.innerHTML += "<h1>Not Licensed</h1><p>"+e.message+"</p>";
}