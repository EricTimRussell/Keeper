<template>
  <form @submit.prevent="createKeep()">
    <div class="form-floating mb-3">
      <input v-model="editable.name" type="text" class="form-control" id="keepName" placeholder="Title">
      <label for="keepName">Name...</label>
    </div>
    <div class="form-floating">
      <input v-model="editable.img" type="url" class="form-control" id="keepImage" placeholder="Image">
      <label for="keepImage">Image Url..</label>
    </div>
    <div class="form-floating my-3">
      <input v-model="editable.tags" type="text" class="form-control" id="keepTags" placeholder="Tags">
      <label for="keepTags">Tags...</label>
    </div>
    <div class="form-floating">
      <textarea v-model="editable.description" class="form-control" placeholder="keep description" id="keepDescription"
        style="height: 100px"></textarea>
      <label for="keepDescription">Description...</label>
    </div>
    <div class="my-3">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Submit</button>
    </div>
  </form>


</template>


<script>
import { Modal } from "bootstrap";
import { ref } from "vue";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          const formData = editable.value
          await keepsService.createKeep(formData)
          Modal.getOrCreateInstance('#keep').hide()
        } catch (error) {
          Pop.error(error, "Creating Keep")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>