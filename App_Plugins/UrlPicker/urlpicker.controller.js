angular.module("umbraco")
    .controller("UrlPicker",
    function ($scope, dialogService) {
      if (!$scope.model.value)
        $scope.model.value = { title: '', internalLink:0, internalLinkName: '', externalUrl: 'http://', isInternal: true, newWindow: false };

      $scope.switch = function () {
        $scope.model.value.isInternal = !$scope.model.value.isInternal;
      };

      $scope.selectContent = function () {
        var d = dialogService.contentPicker({
          scope: $scope,
          multipicker: false,
          callback: function (data) {
            $scope.model.value.internalLink = data.id;
            $scope.model.value.internalName = data.name;
          }
        });
      };
      $scope.removeContent = function () {
        $scope.model.value.internalLink = 0;
        $scope.model.value.internalLinkName = '';
      }

    });
