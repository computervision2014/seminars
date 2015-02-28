28.02.2015
------
Gist for box filter
https://gist.github.com/7052c07a71df7a3a1c64


Семинар 01
------
####Базовые операции над изображением.

Необходимо реализовать 3 метода в классе ```ImageProcessing.cs```:
 1. ```setGrayscale()``` - перевести изображение в градации серого, x = 0.299R + 0.587G + 0.114B
 2. ```setInvert()``` - инвертировать изображение, т.е. 255 минус текущее значение
 3. ```setRedFilter()``` - вывести только красный канал, т.е. каналы G и B занулить, R и A не трогать

**Важно**: дефолтный формат - **Bgra** (Blue, Geen, Red, Alpha)
