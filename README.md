# Shaper

library for recognizing foreground shapes (shapes which are over any others).
## Algorithm ideas
- Last drawn shapes go first.
- Check bounding boxes before complex calculations. 

## Possible ways to improve
- Create generic rules container.
- If there is no free space on surface for line with minimal length then stop processing.

## Progest structure

### Shaper 

It is a library to find foreground shapes.

- Shaper.ForegroundShapeChecker.FindForegroundShapes  
- Shaper.ForegroundShapeChecker.FindForegroundShapesAsync
- Shaper.Generator.GetShapes

### ShaperTests 

It is NUnit tests for solution.  

### ShaperClient

It is a UI to easy demonstrate the result.

# Screenshots

![Alt text](Result/origin.png?raw=true)
![Alt text](Result/result.png?raw=true)

## Requirements

- Find N or All foreground shapes, which meet threshold (minimal area)
- Working asynchronously results should be available “on the fly”, demonstrating the foreground shapes as soon as they are recognized
- API shoud be thread-safe and that should be proven by the tests
- Create generator for set of shapes to have the examples for testing

## Constraints

It should be 100% native .Net solution.