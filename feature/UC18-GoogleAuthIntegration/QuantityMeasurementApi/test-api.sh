#!/bin/bash
BASE_URL="http://localhost:5000"

#JWT token from Google login
TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0aXdhcmlvamFzNTc4QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0aXdhcmlvamFzNTc4IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImp0aSI6IjMwOTk2OGIxLTkzMTAtNGMyYy04MTQ2LTVhZjUwYTc1MGUxYyIsImV4cCI6MTc3NTAyNDA1NywiaXNzIjoiUXVhbnRpdHlNZWFzdXJlbWVudEFwaSIsImF1ZCI6IlF1YW50aXR5TWVhc3VyZW1lbnRDbGllbnRzIn0.seswbGyogYHnlxkznwl3VOwZKExEBeSnOi8kjlngfCc"

echo "=== 1. Testing Compare (5 ft vs 60 in) ==="
curl -s -X POST $BASE_URL/api/Quantity/compare \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"first":{"value":5,"unit":"ft","measurementType":"Length"},"second":{"value":60,"unit":"in","measurementType":"Length"}}' | jq '.isEqual'
echo ""

echo "=== 2. Testing Add (5 ft + 12 in) ==="
curl -s -X POST $BASE_URL/api/Quantity/add \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"first":{"value":5,"unit":"ft","measurementType":"Length"},"second":{"value":12,"unit":"in","measurementType":"Length"},"targetUnit":"ft"}' | jq '.result'
echo ""

echo "=== 3. Testing Convert (1 ft to inches) ==="
curl -s -X POST $BASE_URL/api/Quantity/convert \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"source":{"value":1,"unit":"ft","measurementType":"Length"},"targetUnit":"in"}' | jq '.result'
echo ""

echo "=== 4. Testing Subtract (10 ft - 6 in) ==="
curl -s -X POST $BASE_URL/api/Quantity/subtract \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"first":{"value":10,"unit":"ft","measurementType":"Length"},"second":{"value":6,"unit":"in","measurementType":"Length"},"targetUnit":"ft"}' | jq '.result'
echo ""
