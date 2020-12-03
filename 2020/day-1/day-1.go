package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"sort"
	"strconv"
	"strings"
)

func numbersFromTxt(fileName string) []int {
	content, err := ioutil.ReadFile(fileName)
	if err != nil {
		log.Fatal(err)
	}
	lines := strings.Split(string(content), "\n")
	numbers := make([]int, 0)
	for _, line := range lines {
		number, err := strconv.Atoi(line)
		if err != nil {
			log.Fatal(err)
		}
		numbers = append(numbers, number)
	}
	return numbers
}

func find2ElementsInSlice(target int, numbers []int) (int, int) {
	for i := 0; i < len(numbers); i++ {
		for j := len(numbers) - 1; j >= 0; j-- {
			fmt.Printf("%d %d\n", numbers[i], numbers[j])
			if numbers[i]+numbers[j] == target {
				return numbers[i], numbers[j]
			}
		}
	}
	return 0, 0
}

func find3ElementsInSlice(target int, numbers []int) (int, int, int) {
	for i := 0; i < len(numbers); i++ {
		for j := 0; j < len(numbers); j++ {
			for k := 0; k < len(numbers); k++ {
				if numbers[i]+numbers[j]+numbers[k] == target {
					return numbers[i], numbers[j], numbers[k]
				}
			}
		}
	}
	return 0, 0, 0
}

func main() {
	numbers := numbersFromTxt("input.txt")
	sort.Ints(numbers)
	target := 2020

	number1, number2 := find2ElementsInSlice(target, numbers)
	fmt.Printf("%d + %d = 2020\n%d\n", number1, number2, number1*number2)

	number1, number2, number3 := find3ElementsInSlice(target, numbers)
	fmt.Printf("%d + %d + %d = 2020\n%d\n", number1, number2, number3, number1*number2*number3)
}
