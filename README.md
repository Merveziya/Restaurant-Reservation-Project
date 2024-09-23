<h3> What is the Restaurant Booking System? </h3>
This project is a restaurant booking assistant powered by Semantic Kernel and Azure OpenAI. It allows users to search for restaurants based on cuisine and location, and book a table at their preferred restaurant. The assistant dynamically interacts with the user, asking questions when needed and processing bookings in real-time.

<h2> How it Works &#129155; </h2>

- **Restaurant Search:** The assistant provides a list of restaurants based on the cuisine type and location input by the user.
For example, users can search for "Italian" restaurants in "New York" or "Kebab" restaurants in "Istanbul."
- **Automatic Function Calling:** The system automatically invokes relevant kernel functions like restaurant search or booking through Azure OpenAI, ensuring a seamless conversation flow.
- **Booking System:** Users can book a table at a restaurant by providing details like restaurant name, booking date, time, number of people, and their name.
- **World Time API Integration:** For bookings, the system fetches the current time for the restaurant's location using the World Time API, ensuring accurate time zone handling.

  
![openapi](https://github.com/user-attachments/assets/571abedc-a1f4-43bc-b33f-701d1528b113)
![OpenApi2](https://github.com/user-attachments/assets/6c6db951-4201-4337-8b52-06f3e31a5263)
<h2> Plugins & Integration &#129155; </h2>

- **Bookings Plugin:**
The BookingsPlugin is responsible for searching restaurants and creating bookings.
It uses a dummy restaurant list for demonstration purposes, but can be extended to integrate with actual booking systems.
- **OpenAPI Integration:**
The plugin uses the World Time API to fetch the current time in Istanbul when making a booking.
- **Azure OpenAI Integration:**
The assistant is powered by Azure OpenAI's gpt-35-turbo, which generates responses and invokes kernel functions to provide a conversational experience.

