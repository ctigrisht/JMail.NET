# JMail.NET (WIP)
A simple to implement JSON based alternative to Emails and SMTP.

## Setup
Add a TXT DNS record on your root domain name containing the IP:PORT combination of your Relay

TXT: `JMAIL=123.456.789.123:6051,987.654.321.987:6051`

A relay sends the mail to `username#domain.com` the first IP listed in the "JMAIL" TXT record of the domain `domain.com` and is forwarded to the `username` user, the other IP's are there to support secondary SEND-ONLY relays
