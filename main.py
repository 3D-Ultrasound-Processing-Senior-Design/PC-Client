import PySimpleGUI as sg

window_title = "IMU GUI"
button_text = "close window"

button = [[sg.Button(button_text)]]

window = sg.Window(title=window_title, layout=button, margins=(800, 400))

# Event loop
while True:
    event, values = window.read()
    if event == button_text or event == sg.WIN_CLOSED:
        break

window.close()