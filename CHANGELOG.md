# 0.0.0 -> 1.0.0
## Renamed the list fields in `TSSMsg`:
- `gpumsgs` -> `GPS`
- `imumsgs` -> `IMU`
- `simulationstates` -> `EVA`

They now match the fields the TSS sends once more

Any event handlers subscribed to `OnTSSTelemetryMessage` should be updated.

## Renamed the `EVASimState` type to `EVAMsg`
`EVAMsg` better fits with the other types.

## Updated SampleScene
Works with the new changes. Also cleaned it up a little.
 
## Added fields for UIA related messages to `TSSMsg` class (`UIA` and `UIA_CONTROL`)
**Note:** These fields contain some redundant fields and will likely be changed. As such, they are not ready for use just yet.

## Upcoming:
- More polished UIA related fields
- Potentially a change from JSONUtility to Json.NET (aka Newtonsoft.Json) for better deserializing (such as null fields and enums)
