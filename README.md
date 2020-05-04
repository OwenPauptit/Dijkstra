# Dijkstra's Shortest Path Algorithm

Dijkstra's Shortest Path Algorithm is a method of finding for finding the shortest paths between nodes in a graph. In this implementation, however, I use a matrix. The program will find the shortest path from a start point to an end point while avoiding obstcales (walls).

It can either:
 - Randomly generate a start point, end point and walls
 - Load a text file that describes a particular set up
 - Take a series of inputs from the user indicating the coordinates of different points in the matrix
 
It has been created so that diagonals are included, but have 1.5 time the weight of travelling in a straight line.

For it to work in the C# console, the matrix is a multidimensional array of colours. This is printed every refresh, where '██' represents each cell/node. The green node is the start, the red node is the end, and the white nodes are walls.

Text files can be created to make custom grids, the start is represented by a capital 'S', the end is represented by a capital 'F' and walls are represented by capital 'X's.
