# POS Console App
A point of sale is a simple project developed with C#. It have some basic functionalities like an admin can add products with stock and price and an user can buy product from available stocks.

Create a predefined list of items in stock.

`items = ["pen", "shirt", "cap"]`

Add a price for each item, you can create another array that holds prices, but keep order fixed.

`prices = [5, 100, 50]`

In the data above, 5 is the price of a pen, 100 is the price of a shirt.

When you launch the application display the list of items with their price in the console. A sample is given below.
<pre>
1 pen   5

2 shirt 100

3 cap   50

0 proceed to checkout
</pre>
The user will enter which item he wants to buy and then the number of items he wants to buy and also show item name.  A sample input is below
Please enter item to buy
1
Please enter quantity of pens to buy
5
This means that the user will buy 5 pens
After a selection is done, the console will display the list of items with their prices again.
Lets say the user then enters
2
1
This means he bought a shirt.
The user can proceed to checkout by entering 0.
When the user chooses to proceed to checkout, you will display all the items bought and also the total expenditure, just like the ones we receive in shops. In our case the user bought 5 pens and 2 shirts, so the sample output should be
<pre>
item              quantity      unit price     sum

pen               5             5              25

shirt             1             100            100

total payment                                  125
</pre>
 

After completing the basic version, try adding the number of stock in the inventory. In the initial data, add another array called stock.

`stock = [5, 10, 15]`

The above data means that there are 5 pens, 10 shirts and 15 caps. A user cannot buy more than 5 pens as there is not enough stock.
A user cannot buy more than 5 pens as there is not enough stock.
Let's say the user first buys 1 cap, then if the user selects cap again, he can buy a maximum of 4 caps.
You have to update the items in stock after each purchase.
Next add an admin part to the application. The admin will be able to update stock and create new item.
So the initial screen of the application should be:
For Admin press 0, For customer press 1
