# Experiment Protocol

## Goal
Measure behavior of five Hummingbird inference agents sharing different amounts of flower resources.

## Fixed variables
- 5 birds
- Same trained ONNX policy
- Inference mode
- Crowding distance: 0.50 units
- Same experimental island
- Same environment/agent settings

## Independent variable
Active flowers: 20, 15, 10, 5.

## Randomization
Flower indices are shuffled and the requested number is activated.

## Completion
A run is complete when all active flowers are depleted.

## Recorded metrics
Experiment ID, birds, active flowers, nectar, flowers depleted, duration, crowding distance, crowding events, crowding pair-time, crowding ratio, maximum simultaneous crowded pairs.

## Reproducibility
For stronger future experiments, record/fix random seeds and repeat each condition multiple times.
