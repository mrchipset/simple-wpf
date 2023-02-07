#pragma once

__declspec(dllexport) void Foo();
__declspec(dllexport) int Add(int x, int y);
__declspec(dllexport) double Sum(const double* arr, int len);
__declspec(dllexport) void ChangeContent(double* arr, int len);