#pragma once

typedef struct _Student
{
	char name[32];
	int gender;
	int mark;
} Student;

__declspec(dllexport) void Foo();
__declspec(dllexport) int Add(int x, int y);
__declspec(dllexport) double Sum(const double* arr, int len);
__declspec(dllexport) void ChangeContent(double* arr, int len);
__declspec(dllexport) void FillStudents(Student* students, int len);
