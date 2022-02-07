import random
from random import randint, randrange
from datetime import timedelta
import time

def uniqueid():
    seed = random.getrandbits(32)
    while True:
       yield seed
       seed += 1


 

def str_time_prop(start, end, format, prop):
    stime = time.mktime(time.strptime(start, format))
    etime = time.mktime(time.strptime(end, format))
    ptime = stime + prop * (etime - stime)
    return time.strftime(format, time.localtime(ptime))

def random_date(start, end, prop):
    return str_time_prop(start, end, '%m/%d/%Y %I:%M %p', prop)
    



def getTime():
    return  "{0}:{1}".format(str(randint(0,23)).rjust(2, '0'),str(randint(0,59)).rjust(2, '0'))

