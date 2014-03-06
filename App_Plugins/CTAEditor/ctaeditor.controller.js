angular.module("umbraco")
    .controller("CTAEditor",
    function ($scope, dialogService, imageHelper) {
      if (!$scope.model.value)
        $scope.model.value = [];

      $scope.add = function () {
        $scope.model.value.push({ image: null, heading: '', description: '', internalLink:0, internalName: '', externalUrl: '', isInternal: true, newWindow: false });
      }

      $scope.remove = function (index) {
        $scope.model.value.splice(index, 1);
      }

      $scope.switch = function (cta) {
        cta.isInternal = !cta.isInternal;
      };

      $scope.selectContent = function (cta) {
        var d = dialogService.contentPicker({
          scope: $scope,
          multipicker: false,
          callback: function (data) {
            cta.internalLink = data.id;
            cta.internalName = data.name;
          }
        });
      };
      $scope.removeContent = function (cta) {
        cta.internalLink = 0;
        cta.internalName = '';
      }

      $scope.selectMedia = function (cta) {
        dialogService.mediaPicker({
          multiPicker: false,
          callback: function (data) {
            cta.image = data;
            cta.image.src = imageHelper.getImagePropertyValue({ imageModel: data });
            cta.image.thumbnail = imageHelper.getThumbnailFromPath(data.src);
          }
        });
      };
      $scope.removeMedia = function (cta) {
        cta.image = null;
      }

    });
