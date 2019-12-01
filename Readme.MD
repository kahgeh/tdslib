# Background

It's 2019 and there's still no mock utility for Sql Server. This is an attempt to build just that after waiting for more than 10 years

# Progress

## Done
1. Auth Handshake - Able to read first PreLogin request

e.g.
```
Packet - PreLogin
  Type: 18 
  Status: 1
  Length: 94
  Channel: 0
  PacketNumber: 1
  Window: 0

  Option - VERSION
    Token: 0
    OffSet: 36
    Length: 6
    Version: 4.188.19-34484
```

# Next
1. Auth Handshake - Send first PreLogin response
2. Auth Handshake - Read more PreLogin request
3. Auth Handshake - Send login ack response
4. Read SQL Batch

# References

1 Wireshark provide TDS inspection
2 Specification - https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec

