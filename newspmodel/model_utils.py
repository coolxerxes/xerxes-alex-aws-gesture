from pre_utils import spline_coeff
import pandas as pd
import numpy as np
from sklearn.preprocessing import StandardScaler
from sklearn.ensemble import RandomForestClassifier
from pickle import dump, load



def extract_feature(name):
#read file and extract feature    
    degree=3
    # we have 3 dim in quater,3 dim in Accel, and 3 dim in bending(remove 2 fingers). So 9 
    dim=9   #it is the number of sensor streams
    # the number of coeff (extracted features) from each dim is nk+2. Henc, total of features = (nk+2)*8
    nk=6   #number of knots, the larger nk the more fitted spline 
    features = (nk+2)*dim
    samples=1
    data = np.zeros(shape=(samples,features))
    mypath="C:\\Users\\Jo0od\\CaptoNew\\CaptoStream\\"
    #quater file
    location = 0  # this index is to iterate in the columns of one row 
    myfile = mypath + "Quaternion_" +name +".csv"
    df1 = pd.read_csv(myfile,sep=";", header=None)
    df2=df1.iloc[:,1:4]
    for col in df2:
        x1=df2[col]
        bs=spline_coeff(x1,nk,degree)
        data[0][location:location+bs.c.shape[0]]=bs.c
        location = location + bs.c.shape[0]
    #Accel file
    myfile = mypath + "Accel_" +name +".csv"
    df1 = pd.read_csv(myfile,sep=";", header=None)
    df2=df1.iloc[:,1:4]
    for col in df2:
        x1=df2[col]
        bs=spline_coeff(x1,nk,degree)
        data[0][location:location+bs.c.shape[0]]=bs.c
        location = location + bs.c.shape[0]
    #Bending file
    myfile = mypath + "Bending_"  +name +".csv"
    df1 = pd.read_csv(myfile,sep=";", header=None)
    df2=df1.iloc[:,2:5]    #remove thumb(1) and pinky(5), so take:2,3,4
    for col in df2:
        x1=df2[col]
        bs=spline_coeff(x1,nk,degree)
        data[0][location:location+bs.c.shape[0]]=bs.c
        location = location + bs.c.shape[0] 
 
#standardization
    scalerName = "C:\\Users\\Jo0od\\CaptoNew\\scaler.pkl"
    scaler = load(open(scalerName, 'rb'))
    scaled=scaler.transform(data)  #scaled is numby array                                                                          
    
    return scaled

def make_prediction(data):
# make prediction based on pickled model
    modelName = "C:\\Users\\Jo0od\\CaptoNew\\finalized_model.pkl"
    loaded_model = load(open(modelName, 'rb'))
    result = loaded_model.predict(data)
    #print("The predicted sign is: ",result[0])
    return result[0]




