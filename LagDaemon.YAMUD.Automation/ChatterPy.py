import time
import random

class ChatterPy:
    def __init__(self, chat_hub):
        self.chat_hub = chat_hub

    def send_random_message(self):
        messages = [
            "Hello, everyone!",
            "How's everyone doing today?",
            "Any exciting news to share?"
        ]
        random_message = random.choice(messages)
        self.chat_hub.send_notification_message(random_message)

def main(chat_hub):
    chatter_py = ChatterPy(chat_hub)
    while True:
        chatter_py.send_random_message()
        time.sleep(60)  # Send a message every 60 seconds

# This function is required to create an instance of ChatterPy
def create_instance(chat_hub):
    return ChatterPy(chat_hub)
