# BostonHacks2024

###### 

###### 

###### Video Walkthrough available [here](https://www.youtube.com/watch?v=NQ5OTRC5FMQ)

# Requirements:

* This Project  
* Unity  
* ML-Agent Toolkit  
* Python, PyTorch, ML-Agents

# Installation:

* Install Unity Hub and Unity  
* Install ML-Agents Package in Unity  
* Install Python, PyTorch, and ML-Agents
```
cd <Unity Project Directory>
py -m venv venv
venv\scripts\activate
py -m pip install --upgrade pip
pip install mlagents
pip install torch torchvision torchaudio
pip install protobuf==3.20.3
mlagents-learn --help
```
# How To Use:

* Open the Project
* Open a ML-Agents scene
* Select a Behavior Type under Behavior Parameters in a Agent Prefab
* Select a Model if nessecary
* To start training the model use:

```
mlagents-learn --run-id <file name>
```
