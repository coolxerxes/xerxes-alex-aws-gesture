{
 "cells": [
  {
   "cell_type": "code",
   "execution_count": 21,
   "id": "269a4257",
   "metadata": {},
   "outputs": [
    {
     "name": "stdout",
     "output_type": "stream",
     "text": [
      " * Serving Flask app \"__main__\" (lazy loading)\n",
      " * Environment: production\n",
      "\u001b[31m   WARNING: This is a development server. Do not use it in a production deployment.\u001b[0m\n",
      "\u001b[2m   Use a production WSGI server instead.\u001b[0m\n",
      " * Debug mode: on\n"
     ]
    },
    {
     "name": "stderr",
     "output_type": "stream",
     "text": [
      " * Restarting with watchdog (windowsapi)\n"
     ]
    },
    {
     "ename": "SystemExit",
     "evalue": "1",
     "output_type": "error",
     "traceback": [
      "An exception has occurred, use %tb to see the full traceback.\n",
      "\u001b[1;31mSystemExit\u001b[0m\u001b[1;31m:\u001b[0m 1\n"
     ]
    }
   ],
   "source": [
    "from flask import Flask, request, render_template\n",
    "import pickle\n",
    "\n",
    "app = Flask(__name__)\n",
    "\n",
    "model = pickle.load(open('C:\\\\Users\\\\reemz\\\\Desktop\\\\SeniorProject - Copy\\\\newspmodel\\\\finalized_model.pkl', 'rb'))\n",
    "\n",
    "@app.route('/')\n",
    "def home():\n",
    "    return render_template('index.html')\n",
    "\n",
    "@app.route('/predict',methods=['POST'])\n",
    "def predict():\n",
    "    int_features = [int(x) for x in request.form.values()]\n",
    "    features = [np.array(int_features)]  \n",
    "    prediction = model.predict(features) \n",
    "    result = prediction[0]\n",
    "\n",
    "    return render_template('index.html', prediction=result)\n",
    "\n",
    "if __name__ == \"__main__\":\n",
    "    app.run(debug=True)"
   ]
  },
  {
   "cell_type": "code",
   "execution_count": 22,
   "id": "6f3d6d5f",
   "metadata": {},
   "outputs": [
    {
     "ename": "SystemExit",
     "evalue": "1",
     "output_type": "error",
     "traceback": [
      "\u001b[1;31m---------------------------------------------------------------------------\u001b[0m",
      "\u001b[1;31mSystemExit\u001b[0m                                Traceback (most recent call last)",
      "\u001b[1;32m~\\AppData\\Local\\Temp/ipykernel_724/1937615442.py\u001b[0m in \u001b[0;36m<module>\u001b[1;34m\u001b[0m\n\u001b[0;32m     20\u001b[0m \u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m     21\u001b[0m \u001b[1;32mif\u001b[0m \u001b[0m__name__\u001b[0m \u001b[1;33m==\u001b[0m \u001b[1;34m\"__main__\"\u001b[0m\u001b[1;33m:\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[1;32m---> 22\u001b[1;33m     \u001b[0mapp\u001b[0m\u001b[1;33m.\u001b[0m\u001b[0mrun\u001b[0m\u001b[1;33m(\u001b[0m\u001b[0mdebug\u001b[0m\u001b[1;33m=\u001b[0m\u001b[1;32mTrue\u001b[0m\u001b[1;33m)\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0m",
      "\u001b[1;32m~\\anaconda3\\lib\\site-packages\\flask\\app.py\u001b[0m in \u001b[0;36mrun\u001b[1;34m(self, host, port, debug, load_dotenv, **options)\u001b[0m\n\u001b[0;32m    988\u001b[0m \u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m    989\u001b[0m         \u001b[1;32mtry\u001b[0m\u001b[1;33m:\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[1;32m--> 990\u001b[1;33m             \u001b[0mrun_simple\u001b[0m\u001b[1;33m(\u001b[0m\u001b[0mhost\u001b[0m\u001b[1;33m,\u001b[0m \u001b[0mport\u001b[0m\u001b[1;33m,\u001b[0m \u001b[0mself\u001b[0m\u001b[1;33m,\u001b[0m \u001b[1;33m**\u001b[0m\u001b[0moptions\u001b[0m\u001b[1;33m)\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0m\u001b[0;32m    991\u001b[0m         \u001b[1;32mfinally\u001b[0m\u001b[1;33m:\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m    992\u001b[0m             \u001b[1;31m# reset the first request information if the development server\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n",
      "\u001b[1;32m~\\anaconda3\\lib\\site-packages\\werkzeug\\serving.py\u001b[0m in \u001b[0;36mrun_simple\u001b[1;34m(hostname, port, application, use_reloader, use_debugger, use_evalex, extra_files, exclude_patterns, reloader_interval, reloader_type, threaded, processes, request_handler, static_files, passthrough_errors, ssl_context)\u001b[0m\n\u001b[0;32m   1000\u001b[0m         \u001b[1;32mfrom\u001b[0m \u001b[1;33m.\u001b[0m\u001b[0m_reloader\u001b[0m \u001b[1;32mimport\u001b[0m \u001b[0mrun_with_reloader\u001b[0m \u001b[1;32mas\u001b[0m \u001b[0m_rwr\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m   1001\u001b[0m \u001b[1;33m\u001b[0m\u001b[0m\n\u001b[1;32m-> 1002\u001b[1;33m         _rwr(\n\u001b[0m\u001b[0;32m   1003\u001b[0m             \u001b[0minner\u001b[0m\u001b[1;33m,\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m   1004\u001b[0m             \u001b[0mextra_files\u001b[0m\u001b[1;33m=\u001b[0m\u001b[0mextra_files\u001b[0m\u001b[1;33m,\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n",
      "\u001b[1;32m~\\anaconda3\\lib\\site-packages\\werkzeug\\_reloader.py\u001b[0m in \u001b[0;36mrun_with_reloader\u001b[1;34m(main_func, extra_files, exclude_patterns, interval, reloader_type)\u001b[0m\n\u001b[0;32m    426\u001b[0m                 \u001b[0mreloader\u001b[0m\u001b[1;33m.\u001b[0m\u001b[0mrun\u001b[0m\u001b[1;33m(\u001b[0m\u001b[1;33m)\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m    427\u001b[0m         \u001b[1;32melse\u001b[0m\u001b[1;33m:\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[1;32m--> 428\u001b[1;33m             \u001b[0msys\u001b[0m\u001b[1;33m.\u001b[0m\u001b[0mexit\u001b[0m\u001b[1;33m(\u001b[0m\u001b[0mreloader\u001b[0m\u001b[1;33m.\u001b[0m\u001b[0mrestart_with_reloader\u001b[0m\u001b[1;33m(\u001b[0m\u001b[1;33m)\u001b[0m\u001b[1;33m)\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0m\u001b[0;32m    429\u001b[0m     \u001b[1;32mexcept\u001b[0m \u001b[0mKeyboardInterrupt\u001b[0m\u001b[1;33m:\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n\u001b[0;32m    430\u001b[0m         \u001b[1;32mpass\u001b[0m\u001b[1;33m\u001b[0m\u001b[1;33m\u001b[0m\u001b[0m\n",
      "\u001b[1;31mSystemExit\u001b[0m: 1"
     ]
    }
   ],
   "source": [
    "%tb"
   ]
  },
  {
   "cell_type": "code",
   "execution_count": null,
   "id": "603de30f",
   "metadata": {},
   "outputs": [],
   "source": []
  }
 ],
 "metadata": {
  "kernelspec": {
   "display_name": "Python 3 (ipykernel)",
   "language": "python",
   "name": "python3"
  },
  "language_info": {
   "codemirror_mode": {
    "name": "ipython",
    "version": 3
   },
   "file_extension": ".py",
   "mimetype": "text/x-python",
   "name": "python",
   "nbconvert_exporter": "python",
   "pygments_lexer": "ipython3",
   "version": "3.9.7"
  }
 },
 "nbformat": 4,
 "nbformat_minor": 5
}
