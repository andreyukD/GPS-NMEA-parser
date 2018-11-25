# GPS-NMEA-parser

The app allows to decode messages from <a href="https://www.gpsinformation.org/dale/nmea.htm">NMEA data specification</a> and print them
on readable for users format.

Supported sentences:
* GPRMC
* GPGSA
* GPGSV

Example input:
```
$GPRMC,225446,A,4916.45,N,12311.12,W,000.5,054.7,191194,020.3,E*68
$GPGSA,A,3,19,28,14,18,27,22,31,39,,,,,1.7,1.0,1.3*35
$GPGSV,3,1,11,03,03,111,00,04,15,270,00,06,01,010,00,13,06,292,00*74$GPGSV,3,2,11,14,25,170,00,16,57,208,39,18,67,296,40,19,40,246,00*74
$GPGSV,3,3,11,22,42,067,42,24,14,311,43,27,05,244,00,,,,*4D
```

<img src="https://raw.githubusercontent.com/andreyukD/GPS-NMEA-parser/master/readme_assets/1.png">
