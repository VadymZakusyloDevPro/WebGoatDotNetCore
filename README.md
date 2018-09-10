please use this url for demonstration  https://localhost:44319/Home/Contact?url=https://github.com/ open redirect attack

SCS0027 - Open Redirect
The dynamic value passed to the Redirect should be validated.

This is output from SecurityCodeScan

Severity	Code	Description	Project	File	Line	Suppression State
Warning	SCS0029	Potential XSS vulnerability	WebGoatDotNetCore	C:\projects\WebGoatDotNetCore\WebGoatDotNetCore\Controllers\HomeController.cs	27	Active
Warning	SCS0027	Open redirect: possibly unvalidated input in 1st argument passed to 'Redirect'	WebGoatDotNetCore	C:\projects\WebGoatDotNetCore\WebGoatDotNetCore\Controllers\HomeController.cs	40	Active
Warning	SCS0011	CBC mode is weak	WebGoatDotNetCore	C:\projects\WebGoatDotNetCore\WebGoatDotNetCore\Controllers\HomeController.cs	49	Active
