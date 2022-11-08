<template>
  <form>
    <div class="form-floating mb-3">
      <input type="title" class="form-control" id="floatingInput" placeholder="Title">
      <label for="floatingInput">Title...</label>
    </div>
    <div class="form-floating">
      <input type="password" class="form-control" id="image" placeholder="Image">
      <label for="floatingPassword">Image Url..</label>
    </div>
    <div class="form-floating my-3">
      <input type="password" class="form-control" id="tags" placeholder="Tags">
      <label for="floatingPassword">Tags...</label>
    </div>
    <div class="form-floating">
      <textarea class="form-control" placeholder="keep description" id="Description" style="height: 100px"></textarea>
      <label for="floatingTextarea2">Keep Description</label>
    </div>
    <div class="my-3">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="button" class="btn btn-primary">Submit</button>
    </div>
  </form>


</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>