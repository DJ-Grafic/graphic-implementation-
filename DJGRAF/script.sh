#!/bin/bash

cd C# 

dotnet run 

mv test1.rbm ../

cd ..

python3 imageviewer.py test1.rbm

xdg-open dj_graphic.png