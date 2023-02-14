# Shaper

library for recognizing foreground shapes (shapes which are over any others).

## Requirements

- Find N or All foreground shapes, which meet threshold (minimal area)
- Simple extension by other types of shapes
- Working asynchronously results should be available “on the fly”, demonstrating the foreground shapes as soon as they are recognized
- API shoud be thread-safe and that should be proven by the tests
- Create generator for set of shapes to have the examples for testing
- Describe design approach, strong and week features, API reference.

## Constraints

It should be 100% native .Net solution.

## Progest structure
### Shaper 
It is a library to find foreground shapes.

- Shaper.ShapeSeeker.FindAllForegroundShapes  
- Shaper.ShapeSeeker.FindAllForegroundShapesAsync
- Shaper.Generator.GetShapes

### ShaperTests 

It is NUnit tests for solution.  

### ShaperClient

It is a UI to easy demonstrate the result.

## Algorithm ideas

- scan all next shapes in queue for intersection (done)
- break after first catch (done)
- start from the top of queue and break after running out of space for a new shape (done)
- skip shapes that far apart (done with bounding box)

# Possible ways to improve
- use multithreading for sets of shapes (consider number of processor cores) 
- if there is no free space for line with minimal length then stop processing.
- draw the files using html or svg formats to avoid using System.Draving.Common.dll.
- create generic rules container.
- create visualizer to draw shapes.

# Screenshots
![Alt text](Result/origin.png?raw=true)
![Alt text](Result/result.png?raw=true)