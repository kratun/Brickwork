# Brickwork
Console Application that build brick layer by given input.

## IDE
```
Visual Studio v 16.8.3 or any other software that allows you to start .Net Core App 3.1 applications
```

## How to run
Find and load Brickwork.sln file from the directory where you downloaded it.</br>
When the project is loaded press F5 button (if you use Visual Studio v 16.8.3) from the keyboard or check how to start Console App .Net Core 3.1 with program that you use.</br>

## Description
1. In an effort to make the brickwork really strong we create this app in a way that no brick in it lies on a brick from the previous layer. Each brick has two parts marked with two equal numbers written in the squares of the area that are covered by this brick. All bricks are marked with whole numbers ranging from 1 to the total number of the bricks.</br>
2. On the frist line you should write dimensions of the layer - rows(width) and columns(height). They must be even numbers not exceeding 100. If one or both are out range you will receive an error message. If one or both are not an integer or missing you will receive an error message also. After each error message you have to write the dimensions again.
3. After writing a valid dimensions you will receive a message to write and on the next Nth rows you have to write brick mark on its possition on the layer. If you write less/more brick marked parts than row size or there are not allow mark number, or not valid brick you will receive an error message. After that each error message you have to write row again.</br>
5. Atfer writing the Nth correct row you will receive the result of brickwork. If it no possible to create strong brickwork you will receive a message 
	"-1  
	No solution exist!"
	If there is a solution you will see the next layer where each brick is surrounded by asterisk or dash.
	
	
Other functionality during the brickwork:
- write command "End" to exit;
- write command "Restart" to restart brickwork;
- write command "Repeat" to repeat inputs.

## Enjoy the brickwork
You can repeat the brickwork as many times as you like.</br>
The brickwork will finish when you answer "No" to the question "Do you want to proceed?".</br>