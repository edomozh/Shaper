# Shaper

library for recognizing foreground shapes (shapes which are over any others).

# Requirements

- Find N or All foreground shapes, which meet threshold (minimal area)
- Simple extension by other types of shapes
- Working asynchronously results should be available “on the fly”, demonstrating the foreground shapes as soon as they are recognized
- API shoud be thread-safe and that should be proven by the tests
- Create generator for set of shapes to have the examples for testing
- Describe design approach, strong and week features, API reference, possible ways to impove.

# Constraints

It should be 100% native .Net solution.

# Progest structure

## Visualizer 

Should create an image from List<Shapes>. 

## Shaper 
It is a library to play with shapes.
- Shaper.FindAllForegroundShapes(int amountToFind, IEnumerable<Shape> shapes)  
- Shaper.FindAllForegroundShapesAsync(int amountToFind, IEnumerable<Shape> shapes)
- Shaper.Fabrica.GetList(int count, int minSize, int maxSize, Random randomizer, params Type[] types)...

## ShaperTests 

It is NUnit tests for Shaper.  

## ShaperClient

It is a UI to easy demonstrate the result.  

# Algorithm ideas

- scan all next shapes in queue for intersection
- break after first catch
- use multithreading for sets of shapes (consider number of processor cores)
- start from the top of queue and break after running out of space for a new shape
- skip shapes that far apart

# Things to improve
