import os
import random

import numpy as np
import cv2
import sklearn
import sklearn.model_selection as models
import sklearn.neighbors
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
from sklearn import metrics
from sklearn.naive_bayes import GaussianNB


#Functions

#1. create a 2d list with image and category
def addimagedata(imageCategoryList , categories,loc):
    for category in categories:
        path = os.path.join(loc,category)
        appendImages(imageCategoryList,path,category)
    return imageCategoryList


#2. In here I am adding Image to the list
def appendImages(imageCategoryList,path,category):
    for filename in os.listdir(path):
        image = cv2.imread(os.path.join(path, filename),0)
        image = cv2.resize(image,(100,100))
        image = np.array(image).flatten()
        imageCategoryList.append([image,category])
        # img2 = mpimg.imread(os.path.join(loc,filename))
        # imgplot = plt.imshow(img2)
        # plt.show()
    return imageCategoryList

#3. imageprocess using both ways

def imageProcess(imageCategoryList):
    random.shuffle(imageCategoryList)
    lstimages = []
    lstcategory = []
    addeachtoarray(imageCategoryList, lstimages, lstcategory)
    trainingX,testingX,trainingY,testingY = models.train_test_split(lstimages,lstcategory,test_size=0.25 ,random_state=40) # 25% TEST 75% TRAIN
    KnearestNegibor(trainingX,testingX,trainingY,testingY)
    GaussianNBC(trainingX,testingX,trainingY,testingY)


def addeachtoarray(imageCategoryList,lstimages,lstcategory):
    for image,category in imageCategoryList:
        lstimages.append(image)
        lstcategory.append(category)
    return lstimages,lstcategory

def KnearestNegibor(trainingX,testingX,trainingY,testingY):
    knnModel = sklearn.neighbors.KNeighborsClassifier(n_neighbors=5)
    knnModel.fit(trainingX , trainingY)
    predict = knnModel.predict(testingX)
    print("KNN Accuracy:", metrics.accuracy_score(testingY, predict))

def GaussianNBC(trainingX,testingX,trainingY,testingY):
    model = GaussianNB()
    model.fit(trainingX , trainingY)
    predict = model.predict(testingX)
    print("Gaussian Accuracy:", metrics.accuracy_score(testingY, predict))

# Firstly I get the images.

loc = 'images'
categories =["calf","cat","cow","dog","donkey","dude","fish","fox","hand","plane","rabbit","stingray","tool","yoda"]
imageCategoryList = []
addimagedata(imageCategoryList,categories,loc)
imageProcess(imageCategoryList)




