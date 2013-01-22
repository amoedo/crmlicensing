if (typeof (CLBW) == "undefined")
{ CLBW = { __namespace: true }; }
//This will establish a more unique namespace for functions in this library. This will reduce the 
// potential for functions to be overwritten due to a duplicate name when the library is loaded.
CLBW.VatChecker =
    {

        CheckVat: function (htmlDiv) {

            $.ajax({
                url: 'http://ec.europa.eu/taxation_customs/vies/vatResponse.html?memberStateCode=' + encodeURIComponent(Country.value) + '&number=' + encodeURIComponent(VatNumber.value),
                type: 'GET',
                success: function (res) {
                    var fieldSet = $(res.responseText).find('fieldset')[0]
                    var table = $(fieldSet).find('table')[0].outerHTML;
                    htmlDiv.innerHTML = table;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    htmlDiv.innerHTML = "<p>An error has ocurred</p><p>" + errorThrown + "</p>";
                }
            });
        }
    }