#!/bin/bash

cd C# 

dotnet run 

mv test.rbm ../

cd ..

python3 imageviewer.py test.rbm

xdg-open dj_graphic.png