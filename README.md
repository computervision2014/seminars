28.02.2015
------
1. Gist for box filter
https://gist.github.com/7052c07a71df7a3a1c64
2. Gist for boxBlur_4, explain what happens there https://gist.github.com/esibirtseva/39bf5d00ad597aa78c4b


Семинар 01
------
####Базовые операции над изображением.

Необходимо реализовать 3 метода в классе ```ImageProcessing.cs```:
 1. ```setGrayscale()``` - перевести изображение в градации серого, x = 0.299R + 0.587G + 0.114B
 2. ```setInvert()``` - инвертировать изображение, т.е. 255 минус текущее значение
 3. ```setRedFilter()``` - вывести только красный канал, т.е. каналы G и B занулить, R и A не трогать

**Важно**: дефолтный формат - **Bgra** (Blue, Geen, Red, Alpha)
