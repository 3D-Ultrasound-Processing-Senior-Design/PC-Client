# 3D Ultrasound Senior Design Project

This repository holds the software for ENSC24-43's PC-Client software.

## Setting up your development environment:

### Talking to the part:

- You need to install [this driver] for your windows PC to communicate with the LPMS. (the universal one)

### Conda

1. First, if you haven't already, download and install conda, a python environment and package manager
  at this [link].
    - (Optionally) you can also install the [Anaconda Navigator] to view and modify your python environments.

2. Create the conda env. You can do this in the Anaconda Navigator buy clicking `import`, or you can do it from
   the terminal with the following commands:
    ```console
    conda env create -f environment.yml
    conda activate sd_pc_client_env
    ```
3. Once you've activated the environment, you will need to select the `python interpreter`. Do this by clicking `ctrl+shift+P` and
   typing `Python: Select Interpreter`. 
   - `(sd_pc_client_env)` should appear as one of the options. Select it.

### Running the project

- To run the project, click the run option in the toolbar above. Click `Start Debugging` and select the `PC-Client-App` configuration. Click the file
  you want to run, and select `run`.


## GUI Technologies

- For simple things, the team might consider using [pysimplegui].
  - This python package is great for pop-up windows, and might also be good for creating a file explorer.
- For more complex things, like the live-view of the probe, it might be better to use [`Kivy`].

[Anaconda Navigator]:https://docs.anaconda.com/free/navigator/install/
[link]:https://www.anaconda.com/download/

[pysimplegui]:https://pypi.org/project/PySimpleGUI/
[`Kivy`]:https://kivy.org/

[this driver]:https://www.silabs.com/developers/usb-to-uart-bridge-vcp-drivers?tab=downloads