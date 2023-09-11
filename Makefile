_BACKEND:=vulkan
#_BACKEND:=opengl
#_BACKEND:=gles

all: clean deps build

.PHONY: clean
clean:
	@go clean && rm -rf build

.PHONY: deps
deps:
	@go mod tidy

.PHONY: build
build:
	@echo Building using $(_BACKEND)
	@CGO_ENABLED=1 go build -tags $(_BACKEND)

.PHONY: docs
docs:
	@godoc