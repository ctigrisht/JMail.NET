# JMail.NET (WIP)
A simple to implement JSON based alternative to Emails and SMTP.

## How it works
### General
a `JMail (JsonMail)` is a protocol that works by sending a JSON serialized object containing information about the sender and recipient to a `Relay (REST API)`, in order to verify that the sender is authorized to send from that domain name, a DNS record verfification takes place. 

### Encryption
Each `Relay` generates an RSA (OaepSHA512) public key on startup, that key is used to encrypt data by the sender for safe transport, the key is requested from the receiver `Relay` whenever a `JMail` is sent.


## Formats
### Addresses
A `JMail` address come in the `username#domain.com` format.

### The `JMail` object
TODO

## Setup
Add a TXT DNS record on your root domain name containing the IP:PORT combination of your Relay

TXT: `JMAIL=123.456.789.123:6051,987.654.321.987:6051`

A relay sends the mail to `username#domain.com` the first IP listed in the "JMAIL" TXT record of the domain `domain.com` and is forwarded to the `username` user, the other IP's are there to support secondary SEND-ONLY relays


