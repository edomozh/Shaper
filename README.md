# Shaper
library to recognize foreground shapes (shapes which are over any others)

# Requirements
- Find N or All foreground shapes, which meet threshold (minimal area)
- Simple extension by other types of shapes
- Working asynchronously results should be available “on the fly”, demonstrating the foreground shapes as soon as they are recognized
- Your API shoud be thread-safe and that should be proven by the tests
- Create generator for set of shapes  to have the examples for testing
- Please, provide read me file, describing your design approach, strong and week features, API reference, possible ways to impove.

# Constraints
It should be 100% native .Net solution 

# Progest structure
Visualizer - create an image to show all shapes on the screen
Shaper - Main library with two public methods
- FindAllForegroundShapes(int numberOfShapes, IEnumerable<Shape> shapes)
- FindAllForegroundShapesAsync(int numberOfShapes, IEnumerable<Shape> shapes)
ShaperTests - NUnit tests for Shaper
ShaperClient - UI winform to easy demonstrate the result

# Algorithm ideas
scan all next shapes in queue for intersection
break after first catch
use multithreading for sets of shapes (consider number of processor cores)
start from the top of queue and break after running out of space for a new shape
