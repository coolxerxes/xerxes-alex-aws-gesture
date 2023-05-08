import numpy as np
from statistics import mean
from scipy.interpolate import make_lsq_spline
import matplotlib.pyplot as plt



def knot_points1(nknots, x, degree):
#create the knot locations    
    knots = np.linspace(x[0], x[-1], nknots) 
    lo = min(x[0], knots[0]) #we have to add these min and values to conform by adding preceding and proceeding values
    hi = max(x[-1], knots[-1])
    augmented_knots = np.append(np.append([lo]*degree, knots), [hi]*degree)
    return augmented_knots


def knot_points2(nknots, x, degree):
#create the knot locations    
    knots = np.linspace(x[0], x[-1], nknots) 
    return knots


def spline_coeff(x1, nk, degree):   
    y = np.array(x1)
    x = np.array(range(0,len(x1)))
    nknots = nk
    augmented_t = knot_points1(nknots, x, degree)
    bs = make_lsq_spline(x, y, augmented_t, k=degree)
    return bs

def spline_draw(x1, bs):
    y = np.array(x1)
    x = np.array(range(0,len(x1)))
    plt.plot(x, y, '.', label='Observations')
    plt.plot(x, bs(x), '-', label='Model')
    plt.legend()
    plt.grid()
    plt.title('B-Splines')
    plt.show()
    return
    
    