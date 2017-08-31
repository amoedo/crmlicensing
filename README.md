# crmlicensing

## Project Description
This project provides a base implementation for 2 different techniques to implement licensing in Microsoft Dynamics CRM. 

## Project Details

This project started as part of a presentation at the [Extreme CRM Rome 2013](http://www.extremecrm.com) conference. The slides with the details about the project are available here [Building a Licensing Strategy for CRM ISV Solutions](http://www.slideshare.net/marcoamoedo/building-a-licensing-strategy-for-crm-isv-solutions)

The project presents two alternative mechanisms to provide a platform to license ISV solutions on Microsoft Dynamics CRM. In other words, a lightweight IP protection mechanism to enable ISVs to control the usage of their solutions. The two mechanisms are:

* Key Generation: Traditional License Key generation with RSA Signature validation. Enables generation of licenses tied to an organization. It also allows to generate time bound trials.
* License Wall: A solution based on using a web site to store the key components of the solution (js, web resources, etc.) on the cloud and validate the entitlement to access the solution using the organisation name on the request.

*More details and slides coming soon*

*References*
[jsrsasign JavaScript library](http://kjur.github.com/jsrsasign/)

More soon...
