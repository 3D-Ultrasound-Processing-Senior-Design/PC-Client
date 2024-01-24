import PySimpleGUI as sg

window_title = "IMU GUI"
button_text = "close window"

# Set theme to dark green
sg.theme('DarkGreen3')

layout = [
    [sg.Text("This is a test button:")],
    [sg.Button(button_text)]
]

titlebar = sg.Titlebar(background_color="Black")

window = sg.Window(title=window_title, layout=layout, margins=(800, 400), use_custom_titlebar=titlebar)

# Event loop
while True:
    event, values = window.read()
    if event == button_text or event == sg.WIN_CLOSED:
        break

window.close()