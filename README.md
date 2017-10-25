# E-Closings-Integration

OVERVIEW: Windows service for eClosing orders incoming and outgoing. Current implementation only supports First American. Service will load all First American eClosing orders and "track" the orders. When an order status changes in eClosings (ex. Pending to Scheduled) the service will pick up the change, retrieve all of the information updated, generate an integration message, and send an update to Mirth (for more info on Mirth, look at the Mirth-Config project) 

CONTACT: If you have problems, questions, ideas, or suggestions please contact Engineering or IT.