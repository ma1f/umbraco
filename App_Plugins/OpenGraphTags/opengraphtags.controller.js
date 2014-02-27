angular.module("umbraco")
    .controller("OpenGraphTags",
    function ($rootScope, $scope) {
      // hm, was extending Array.prototype, but that appears to cause issues... just plain jane funcs for now
      var combineTags = function (a, b) {
        // if original array does not contain current 2nd array value, add it
        for (var i = 0; i < b.length; i++)
          if (!any(a, function (obj) { return obj.name == b[i].name; }))
            a.push(b[i]);
        return a;
      };
      var any = function (a, func) {
        var i = a.length;
        while (i--)
          if (func(a[i], i))
            return true;
        return false;
      };
      var all = function (a, func) {
        var i = a.length;
        while (i--)
          if (!func(a[i], i))
            return false;
        return true;
      };
      var each = function (a, func) {
        for (var i = 0; i < a.length; i++)
          func(a[i], i);
        return a;
      };

      $scope.defaultfb = [
          { name: 'og:app_id', content: '' },
          { name: 'og:site_name', content: '' },
          { name: 'og:title', content: '' },
          { name: 'og:description', content: '' },
          { name: 'og:image', content: 'http://' }
      ];
      $scope.defaulttc = [
          { name: 'twitter:card', content: 'summary' },
          { name: 'twitter:site', content: '' },
          { name: 'twitter:creator', content: '' },
          { name: 'twitter:title', content: '' },
          { name: 'twitter:description', content: '' },
          { name: 'twitter:image', content: 'http://' }
      ];
      $scope.defaultgp = [
          { name: 'og:title', content: '' },
          { name: 'og:description', content: '' },
          { name: 'og:image', content: 'http://' }
      ];

      if (!$scope.model.value)
        $scope.model.value = [];

      $scope.add = function () {
        $scope.model.value.push({ name: '', content: '' });
      }

      $scope.remove = function (index) {
        $scope.model.value.splice(index, 1);
      }
      // show if all of the default tags for type are currently displayed in the model
      $scope.showfb = function () {
        return all($scope.defaultgp, function (tag, i) { return any($scope.model.value, function (obj, i) { return obj.name == tag.name; }); });
      }
      $scope.showtc = function () {
        return all($scope.defaulttc, function (tag, i) { return any($scope.model.value, function (obj, i) { return obj.name == tag.name; }); });
      }
      $scope.showgp = function () {
        return all($scope.defaultgp, function (tag, i) { return any($scope.model.value, function (obj, i) { return obj.name == tag.name; }); });
      }

      $scope.addFacebookShare = function () {
        combineTags($scope.model.value, $scope.defaultfb);
      }
      $scope.addTwitterCard = function () {
        combineTags($scope.model.value, $scope.defaulttc);
      }
      $scope.addGoogleSnippet = function () {
        combineTags($scope.model.value, $scope.defaultgp);
      }

      //scope.$watch('model', function () {
      //  $scope.tags = $scope.model.value.select(function (obj) { var o = {}; o[obj.name] = obj.content; return o; });
      //  console.log($scope.model.value, $scope.tags);
      //});
      $scope.sync = function () {
        $scope.tags = {};
        each($scope.model.value, function (obj) { $scope.tags[obj.name] = obj.content; });
      }

      $scope.sync();
    });
