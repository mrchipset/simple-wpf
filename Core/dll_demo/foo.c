#include "foo.h"
#include <stdio.h>

void Foo()
{
	printf("Hello World!\n");
}

int Add(int x, int y)
{
	return x + y;
}

double Sum(const double* arr, int len)
{
	double sum = 0.0;
	for (int i = 0; i < len; ++i) {
		sum += arr[i];
	}
	return sum;
}

void ChangeContent(double* arr, int len)
{
	for (int i = 0; i < len; ++i) {
		arr[i] += 1;
	}
}

